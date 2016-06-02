<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Login | {!! config('cms.sitename') !!}</title>

    <!-- Styles -->
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" rel="stylesheet">
    <link href="{!! asset('/css/style.css') !!}" rel="stylesheet">

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
<body id="app-layout">
    <nav class="navbar navbar-default navbar-fixed-top" id="antwerp-menu">
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
                <a href="{{ url('/backend') }}" class="pull-left"><img src="{!! asset('images/logo.svg') !!}" alt="logo" class="grid-height"></a>
                <a class="navbar-brand" href="{{ url('/backend') }}">
                  {!! config('cms.sitename') !!}
                </a>
            </div>

            <div class="collapse navbar-collapse" id="app-navbar-collapse">

                <!-- Right Side Of Navbar -->
                <ul class="nav navbar-nav navbar-right">
                  <li><a href="{{ url('/') }}">
                    <span class="glyphicon glyphicon-log-out" aria-hidden="true"></span>
                    Front=end</a>
                  </li>
                </ul>
            </div>
        </div>
    </nav>

    <div class="container">
        <div class="row">
            <div class="col-md-12">
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
