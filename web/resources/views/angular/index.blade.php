<!doctype html>
<html>
    <head>
        <meta charset="utf-8">
        <title>Project Antwerpen</title>
        <link rel="stylesheet" href="{!! asset('node_modules/bootstrap/dist/css/bootstrap.css') !!}">
        <link rel="stylesheet" href="{!! asset('angular/css/style.css') !!}">


        <!-- Favicons -->
        <link rel="apple-touch-icon" sizes="57x57" href="/apple-touch-icon-57x57.png">
        <link rel="apple-touch-icon" sizes="60x60" href="/apple-touch-icon-60x60.png">
        <link rel="apple-touch-icon" sizes="72x72" href="/apple-touch-icon-72x72.png">
        <link rel="apple-touch-icon" sizes="76x76" href="/apple-touch-icon-76x76.png">
        <link rel="apple-touch-icon" sizes="114x114" href="/apple-touch-icon-114x114.png">
        <link rel="apple-touch-icon" sizes="120x120" href="/apple-touch-icon-120x120.png">
        <link rel="icon" type="image/png" href="/favicon-32x32.png" sizes="32x32">
        <link rel="icon" type="image/png" href="/favicon-96x96.png" sizes="96x96">
        <link rel="icon" type="image/png" href="/favicon-16x16.png" sizes="16x16">
        <link rel="manifest" href="/manifest.json">
        <link rel="mask-icon" href="/safari-pinned-tab.svg" color="#CF0039">
        <meta name="msapplication-TileColor" content="#da532c">
        <meta name="theme-color" content="#cf0039">
    </head>

    <body ng-app="antwerpApp">

    <!-- Fixed navbar -->
    <nav class="navbar navbar-default navbar-fixed-top" id="antwerp-menu">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" ng-init="isCollapsed = true" ng-click="isCollapsed = !isCollapsed">
            <span class="sr-only">Navigatie</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>

          <!-- Branding -->
          <a ui-sref="projects" class="pull-left"><img src="{!! asset('angular/images/logo.svg') !!}" class="grid-height"></a>
          <a class="navbar-brand" ui-sref="home.intro">
            {!! config('cms.sitename') !!}
          </a>

        </div>
        <div class="navbar-collapse collapse" uib-collapse="isCollapsed">
          <!-- Left Side Of Navbar -->
          <!-- <ul class="nav navbar-nav">
            <li><a ui-sref="projects.map">Projecten</a></li>
          </ul> -->

          <!-- Right Side Of Navbar -->
          <ul class="nav navbar-nav navbar-right">

            <li><a ui-sref="projects.map">Projecten</a></li>

            <!-- Authentication Links -->
              <li ng-hide="authenticated"><a ui-sref="user.login">Inloggen</a></li>
              <li ng-hide="authenticated"><a href="{{ url('/register') }}">Registreer (old)</a></li>

              <li class="btn-group" uib-dropdown ng-show="authenticated">
                <a href uib-dropdown-toggle>
                  @{{ currentUser.full_name }} <span class="caret"></span>
                </a>
                <ul class="dropdown-menu" uib-dropdown-menu role="menu" aria-labelledby="single-button">
                  <li ng-show="currentUser.admin == 1"><a href="{{ route('backend.dashboard') }}"><i class="fa fa-btn fa-gear"></i>Backend</a></li>
                  <li><a ui-sref="user.logout"><i class="fa fa-btn fa-sign-out"></i>Log uit</a></li>
                </ul>
              </li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
    </nav>

    <div ui-view class="full-height"></div>
           
    </body>

    <!-- Application Dependencies - Dont forget to add .min in production build -->
    <script src="{!! asset('node_modules/lodash/lodash.js') !!}"></script>
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

    <script src="{!! asset('angular/scripts/controllers/authController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/controllers/logoutController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/controllers/projectController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/controllers/projectsController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/controllers/projectListController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/controllers/projectMapController.js') !!}"></script>
    <script src="{!! asset('angular/scripts/controllers/modalController.js') !!}"></script>
</html>