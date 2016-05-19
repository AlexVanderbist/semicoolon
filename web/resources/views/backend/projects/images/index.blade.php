@extends('layouts.backend')

@section('title', 'Project Foto\'s')

@section('content')

    <div class="btn-group" role="group" style="margin-bottom:15px">
        <a href="{{ route('backend.projects.index') }}" class="btn btn-default">
            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span> Overzicht projecten
        </a>
        <a href="{{ route('backend.projects.edit', $project->id) }}" class="btn btn-default">
            <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Project aanpassen
        </a>
    </div>
        
    <div class="panel panel-default">
        <div class="panel-heading">
            <strong>Foto's: {{ $project->name }}</strong>
        </div>

        <div class="panel-body">
            <div class="form-group">
                @if(! $project->images->isEmpty())
                    <small>Klik op een afbeelding om ze in te stellen als hoofdafbeelding.</small>
                    <div class="row" id="album_images">
                        @foreach($project->images as $image)
                            <div class="col-xs-3">
                                <div class="thumbnail text-right {{ $project->header_image->id == $image->id ? 'active' : ''}}">
                                    <a href="{{ route('backend.projects.{project}.images.destroy', [$project->id, $image->id]) }}" data-method="delete" data-token="{{csrf_token()}}" title="Foto verwijderen">
                                        <span class="glyphicon glyphicon-remove" aria-hidden="true"></span>
                                    </a>
                                    <a href="{{ route('backend.projects.{project}.images.update', [$project->id, $image->id]) }}" data-method="put" data-token="{{csrf_token()}}">
                                        <img src="{{asset($image->filename)}}" alt="Klik om in te stellen als hoofdafbeelding">            
                                    </a>
                                </div>  
                            </div> 
                        @endforeach
                    </div>
                @else 
                    <em>Nog geen foto's toegevoegd.</em>
                @endif
            </div>
        </div>

        <div class="panel-footer">
            {!! Form::open([
                'route' => ['backend.projects.{project}.images.store', $project->id],
                'method'=>'POST',
                'files'=>true
            ]) !!}

            {!! Form::label('images', 'Afbeeldingen uploaden') !!}<br/>

            <div class="input-group">
                <span class="input-group-btn">
                    <span class="btn btn-default btn-file">
                        Bladeren... {!! Form::file('images[]', ['multiple'=>true, 'accept'=>"image/*"]) !!}
                    </span>
                </span>
                <input type="text" class="form-control" readonly>
                <span class="input-group-btn">
                    {!! Form::submit('Foto\'s toevoegen aan project', ['class' => 'btn btn-primary ']) !!}
                </span>
            </div>

            {!! Form::close() !!}
        </div>
    </div>

    <script>

        $(document).on('change', '.btn-file :file', function() {
            var input = $(this),
                numFiles = input.get(0).files ? input.get(0).files.length : 1,
                label = input.val().replace(/\\/g, '/').replace(/.*\//, '');

            input.trigger('fileselect', [numFiles, label]);
        });

        $(document).ready( function() {
            $('.btn-file :file').on('fileselect', function(event, numFiles, label) {
                
                var input = $(this).parents('.input-group').find(':text');
                var log = numFiles > 1 ? numFiles + ' files selected' : label;
                
                if( input.length ) {
                    input.val(log);
                } else {
                    if( log ) alert(log);
                }
                
            });
        });
    </script>
@endsection