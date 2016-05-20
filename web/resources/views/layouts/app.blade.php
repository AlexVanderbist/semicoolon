<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>{!! config('cms.sitename') !!}</title>

    <!-- Fonts -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" rel='stylesheet' type='text/css'>
    <link href="https://fonts.googleapis.com/css?family=Lato:100,300,400,700" rel='stylesheet' type='text/css'>

    <!-- Styles -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet">
    <link href="{!! asset('/css/lightbox.min.css') !!}" rel="stylesheet">
    <link href="{!! asset('/css/style.css') !!}" rel="stylesheet">

    <!-- JavaScripts -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src='http://maps.google.com/maps/api/js?key=AIzaSyDhTfQGWyjyP7vj3t_GFtOrF7-mbGsVLAY&libraries=places'></script>
    

</head>
<body id="app-layout">

    <!-- Fixed navbar -->
    <nav class="navbar navbar-inverse {{ Request::is('/') ? 'navbar-fixed-top' : '' }}">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Navigatie</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>

          <!-- Branding -->
          <a class="navbar-brand" href="{{ url('/') }}">
            {!! config('cms.sitename') !!}
          </a>

        </div>
        <div id="navbar" class="navbar-collapse collapse">
          <!-- Left Side Of Navbar -->
          <ul class="nav navbar-nav">
            <li><a href="{{ route('frontend.projects.index') }}">Projecten</a></li>
          </ul>

          <!-- Right Side Of Navbar -->
          <ul class="nav navbar-nav navbar-right">
            <!-- Authentication Links -->
            @if (Auth::guest())
              <li><a href="{{ url('/login') }}">Inloggen</a></li>
              <li><a href="{{ url('/register') }}">Registreer</a></li>
            @else
              <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">
                {{ Auth::user()->firstname }} <span class="caret"></span>
                </a>

                <ul class="dropdown-menu" role="menu">
                  @if(Auth::check() && Auth::user()->admin)<li><a href="{{ route('backend.dashboard') }}"><i class="fa fa-btn fa-gear"></i>Backend</a></li>@endif
                  <li><a href="{{ url('/logout') }}"><i class="fa fa-btn fa-sign-out"></i>Log uit</a></li>
                </ul>
              </li>
            @endif
          </ul>
        </div><!--/.nav-collapse -->
      </div>
    </nav>


    @yield('content')

<script src="{!! asset('js/lightbox.min.js') !!}"></script>
</body>
</html>
