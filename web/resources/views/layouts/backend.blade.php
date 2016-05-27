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


    <!-- JavaScripts -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <script src='http://maps.google.com/maps/api/js?key=AIzaSyDhTfQGWyjyP7vj3t_GFtOrF7-mbGsVLAY&libraries=places'></script>
    <script src="{!! asset('js/locationpicker.jquery.min.js') !!}"></script>
    <script src="{!! asset('trumbowyg/trumbowyg.min.js') !!}"></script>
    <script src="{!! asset('js/Chart.min.js') !!}"></script>
    <script src="{!! asset('js/backend.js') !!}"></script>

    <!-- Styles -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet">
    <link href="{!! asset('trumbowyg/ui/trumbowyg.min.css') !!}" rel="stylesheet">
    <link href="{!! asset('/css/style.css') !!}" rel="stylesheet">

</head>
<body id="app-layout">
    <nav class="navbar navbar-default">
        <div class="container">
            <div class="navbar-header">

                <!-- Collapsed Hamburger -->
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#app-navbar-collapse">
                    <span class="sr-only">Navigatie</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>

                <!-- Branding Image -->
                <a class="navbar-brand logobrand" href="{{ url('/backend') }}">
                  <img src="{!! asset('images/A_logo_RGB_123x123.jpg') !!}" alt="logo" class="headerlogo">
                </a>
                <a class="navbar-brand namebrand" href="{{ url('/backend') }}">
                <p>{!! config('cms.sitename') !!}</p>
                </a>
            </div>

            <div class="collapse navbar-collapse" id="app-navbar-collapse">
                <!-- Left Side Of Navbar -->
                <ul class="nav navbar-nav">
                    <li><a href="{{ route('backend.projects.index') }}">Projecten</a></li>
                    <li><a href="{{ route('backend.themes.index') }}">Thema's</a></li>
                    <li><a href="{{ route('backend.users.index') }}">Gebruikers</a></li>
                </ul>

                <!-- Right Side Of Navbar -->
                <ul class="nav navbar-nav navbar-right">
                    <!-- Authentication Links -->
                    <!--@if (Auth::guest())
                        <li><a href="{{ url('/login') }}">Login</a></li>
                        <li><a href="{{ url('/register') }}">Registreer</a></li>
                    @else-->

                        <li><a href="{{ url('/') }}">Frontend</a></li>
                        
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">
                                {{ $user->firstname }} <span class="caret"></span>
                            </a>

                            <ul class="dropdown-menu" role="menu">
                                <li><a href="{{ url('/logout') }}"><i class="fa fa-btn fa-sign-out"></i>Log uit</a></li>
                            </ul>
                        </li>
                    <!--@endif-->
                </ul>
            </div>
        </div>
    </nav>

    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h3>@yield('title')</h3>

                @if($errors->any())
                    <div class="alert alert-danger">
                        <strong>Er is iets mis gegaan!</strong>

                        <ul>
                            @foreach($errors->all() as $error)
                                <li>{{ $error }}</li>
                            @endforeach
                        </ul>
                    </div>
                @endif

                @if($status)
                    <div class="alert alert-info">
                        {{ $status }}
                    </div>
                @endif

                @yield('content')
            </div>
        </div>
    </div>

    <script>
        (function() {

          // JS code to create anchor tags with a specific Anguler method

          var laravel = {
            initialize: function() {
              this.methodLinks = $('a[data-method]');

              this.registerEvents();
            },

            registerEvents: function() {
              this.methodLinks.on('click', this.handleMethod);
            },

            handleMethod: function(e) {
              var link = $(this);
              var httpMethod = link.data('method').toUpperCase();
              var form;

              // If the data-method attribute is not PUT or DELETE,
              // then we don't know what to do. Just ignore.
              if ( $.inArray(httpMethod, ['PUT', 'DELETE']) === - 1 ) {
                return;
              }

              // Allow user to optionally provide data-confirm="Are you sure?"
              if ( link.data('confirm') ) {
                if ( ! laravel.verifyConfirm(link) ) {
                  return false;
                }
              }

              form = laravel.createForm(link);
              form.submit();

              e.preventDefault();
            },

            verifyConfirm: function(link) {
              return confirm(link.data('confirm'));
            },

            createForm: function(link) {
              var form = 
              $('<form>', {
                'method': 'POST',
                'action': link.attr('href')
              });

              var token = 
              $('<input>', {
                'type': 'hidden',
                'name': '_token',
                  'value': link.data('token') // hmmmm...
                });

              var hiddenInput =
              $('<input>', {
                'name': '_method',
                'type': 'hidden',
                'value': link.data('method')
              });

              return form.append(token, hiddenInput)
                         .appendTo('body');
            }
          };

          laravel.initialize();

        })();
    </script>
</body>
</html>
