(function() {

    'use strict';

    angular
        .module('antwerpApp')
        .controller('projectController', function ($scope, $rootScope, $state, $stateParams, project, projectService, userService, $interval, mapOptions) {

            // returns google maps icon object with symbol in given color
            var iconSymbol = function (color) {
                return {
                    path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z M -2,-30 a 2,2 0 1,1 4,0 2,2 0 1,1 -4,0',
                    fillColor: color,
                    fillOpacity: 1,
                    strokeColor: '#000',
                    strokeWeight: 2,
                    scale: 1,
               };
      			};

            // bind some scope stuff

            $scope.project = project.data.project;
            $scope.project.latitude = $scope.project.lat;
            $scope.project.longitude = $scope.project.lng;
            $scope.project.icon = iconSymbol($scope.project.theme.hex_color);
            $scope.project.opinions = [];
            $scope.opinionsInitLoading = true;
            $scope.newOpinion = {};
            $scope.postingOpinion = false;
            $scope.map = {
                center: {
                    latitude: $scope.project.lat,
                    longitude: $scope.project.lng
                },
                zoom: 15,
                options: {styles: mapOptions.styleArray}
            };

            // Notifications
            $scope.busyNotificationRequest = false;
            $scope.userNotificationStatus = false;
			if($rootScope.authenticated) {
	            userService.getNotificationStatus($scope.project.id).then(function(response) {
	                $scope.userNotificationStatus = response.data.notificationStatus;
	            });
			}

			$scope.toggleNotifications = function () {
				if($rootScope.authenticated) {
					$scope.busyNotificationRequest = true;
					userService.setNotificationStatus($scope.project.id, ! $scope.userNotificationStatus).then(function(response){
						$scope.userNotificationStatus = response.data.notificationStatus;
						$scope.busyNotificationRequest = false;
					});
				} else {
					$state.go('user.login', {status:'Je moet ingelogd zijn om projecten te volgen.'});
				}
			};

			function loadOpinions () {
	            projectService.opinions($stateParams.id).then(function(response){
	                // opinions loaded; if there are any add them
	                if(response.data.opinions.length) {
						$scope.project.opinions = response.data.opinions;
		                console.log('opinions loaded', $scope.project.opinions);
					}
	            });
			}

			loadOpinions();
			$interval(loadOpinions, 5000);

			$scope.postOpinion = function () {
                if(! $scope.newOpinion.opinion.length) return;

                $scope.postingOpinion = true;
				projectService.postOpinion($stateParams.id, $scope.newOpinion).then(function(response) {
					// posted, now add to object and reload to get new comments as well
					$scope.project.opinions = response.data.opinions;
					$scope.newOpinion = {};
                    $scope.postingOpinion = false;
				}, function(response) {
                    // error
                    if(response.status == 422) {
                        $scope.postingOpinion = false;
                    }
                });
			};

			$scope.removeOpinion = function (id, index) {
				var index = $scope.project.opinions.length - 1 - index;
				projectService.removeOpinion(id).then(function(response) {
					// posted, now add to object and reload to get new comments as well
					console.log(index);
					$scope.project.opinions.splice(index, 1);
				});
			};
        });

})();
