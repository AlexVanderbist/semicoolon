<!doctype html>
<html>
    <head>
        <meta charset="utf-8">
        <title>Project Antwerpen</title>
        <link rel="stylesheet" href="{!! asset('node_modules/bootstrap/dist/css/bootstrap.css') !!}">
    </head>

    <body ng-app="antwerpApp">

        <div class="container">
        	<div ui-view></div>
        </div>        
     
    </body>

    <!-- Application Dependencies - Dont forget to add .min in production build -->
    <script src="{!! asset('node_modules/angular/angular.js') !!}"></script>
    <script src="{!! asset('node_modules/angular-ui-router/release/angular-ui-router.js') !!}"></script>
    <script src="{!! asset('node_modules/satellizer/satellizer.js') !!}"></script>

    <!-- Application Scripts -->
    <script src="{!! asset('angular/scripts/app.js') !!}"></script>
    <script src="{!! asset('angular/scripts/userService.js') !!}"></script>
    <script src="{!! asset('angular/scripts/authController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/logoutController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/projectController.js') !!}"></script>
</html>