<!doctype html>
<html>
    <head>
        <meta charset="utf-8">
        <title>Project Antwerpen</title>
        <link rel="stylesheet" href="{!! asset('node_modules/bootstrap/dist/css/bootstrap.css') !!}">
        <link rel="stylesheet" href="{!! asset('angular/css/style.css') !!}">
    </head>

    <body ng-app="antwerpApp">

    <!-- Fixed navbar -->
    <nav class="navbar navbar-inverse navbar-fixed-top">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Navigatie</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>

          <!-- Branding -->
          <a class="navbar-brand" ui-sref="projects">
            {!! config('cms.sitename') !!}
          </a>

        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <!-- Left Side Of Navbar -->
          <ul class="nav navbar-nav">
            <li><a ui-sref="projects">Projecten</a></li>
          </ul>

          <!-- Right Side Of Navbar -->
          <ul class="nav navbar-nav navbar-right">
            <!-- Authentication Links -->
              <li ng-hide="authenticated"><a ui-sref="login">Inloggen</a></li>
              <li ng-hide="authenticated"><a href="{{ url('/register') }}">Registreer (old)</a></li>

              <li class="btn-group" uib-dropdown>
                <a href uib-dropdown-toggle>
                  @{{ currentUser.full_name }} <span class="caret"></span>
                </a>
                <ul class="dropdown-menu" uib-dropdown-menu role="menu" aria-labelledby="single-button">
                  <li ng-show="currentUser.admin == 1"><a href="{{ route('backend.dashboard') }}"><i class="fa fa-btn fa-gear"></i>Backend</a></li>
                  <li><a ui-sref="logout"><i class="fa fa-btn fa-sign-out"></i>Log uit</a></li>
                </ul>
              </li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
    </nav>

    <div ui-view></div>
           
    </body>

    <!-- Application Dependencies - Dont forget to add .min in production build -->
    <script src="{!! asset('node_modules/angular/angular.js') !!}"></script>
    <script src="{!! asset('node_modules/angular-ui-router/release/angular-ui-router.js') !!}"></script>
    <script src="{!! asset('node_modules/satellizer/satellizer.js') !!}"></script>
    <script src="{!! asset('node_modules/angular-ui-bootstrap/dist/ui-bootstrap.js') !!}"></script>
    <script src="{!! asset('node_modules/angular-sanitize/angular-sanitize.js') !!}"></script>
    <script src="{!! asset('node_modules/angular-simple-logger/dist/angular-simple-logger.js') !!}"></script>
    <script src="{!! asset('node_modules/angular-google-maps/dist/angular-google-maps.js') !!}"></script>

    <!-- Application Scripts -->
    <script src="{!! asset('angular/scripts/app.js') !!}"></script>
    <script src="{!! asset('angular/scripts/userService.js') !!}"></script>
    <script src="{!! asset('angular/scripts/projectService.js') !!}"></script>

    <script src="{!! asset('angular/scripts/authController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/logoutController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/projectController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/projectListController.js') !!}"></script>
</html>