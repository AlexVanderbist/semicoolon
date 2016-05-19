@extends('layouts.backend')

@section('title', 'Project photo\'s')

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
            <strong>{{ $project->title }} Foto's</strong>
        </div>

        <div class="panel-body">

            {!! Form::model($projectImage, [
                'method' => 'post',
                'route' => ['backend.projects.{project}.images.store', $projectImage->id]
            ]) !!}

            <div class="form-group">
                @if(! $project->images->isEmpty())
                    Click to delete an image.
                    <div class="row" id="album_images">
                        @foreach($project->images as $image)
                            <div class="col-xs-3">
                                <div class="thumbnail">
                                    <a href="{{ route('backend.projects.{project}.images.destroy', [$project->id, $image->id]) }}">
                                        <img src="{{$image->filename}}" alt="Click to remove">                                    
                                    </a>
                                </div>  
                            </div> 
                        @endforeach
                    </div>
                @else 
                    <em>No image's yet...</em>
                @endif
            </div>

            {!! Form::close() !!}

        </div>

        <div class="panel-footer">
            {!! Form::open([
                'route' => ['backend.projects.{project}.images.create', $project->id],
                'method'=>'POST',
                'files'=>true
            ]) !!}

            {!! Form::label('images', 'Upload images') !!}<br/>

            <div class="input-group">
                <span class="input-group-btn">
                    <span class="btn btn-default btn-file">
                        Browse... {!! Form::file('images[]', ['multiple'=>true, 'accept'=>"image/*"]) !!}
                    </span>
                </span>
                <input type="text" class="form-control" readonly>
                <span class="input-group-btn">
                    {!! Form::submit('Upload photo\'s to album', ['class' => 'btn btn-primary ']) !!}
                </span>
            </div>

            {!! Form::close() !!}
        </div>
    </div>
    
    <!-- 
        <button type="button" class="btn btn-default" id="open-file-browser">
            <span class="glyphicon glyphicon-plus-sign" aria-hidden="true"></span> Add photo
        </button> 
    -->


    <script>

        // function addImage(url) {
        //     $col = $('<div/>')
        //         .addClass('col-xs-3')
        //         .appendTo('#album_images');

        //     $thumb = $("<div/>").addClass('thumbnail')
        //         .appendTo($col);
        //     $('<img/>').attr('src', url)
        //         .attr('alt', 'Welcome2Altea')
        //         .appendTo($thumb);
        // }

        // $('form').submit(function() {
        //     var imageList = new Array();
        //     $('#album_images img').each(function(){
        //         imageList.push($(this).attr('src'));
        //     });
        //     $('#hidden-image-list').val(JSON.stringify(imageList));
        //     //return false;
        // });

        // $('#open-file-browser').click(function(e) {
        //     e.preventDefault();
        //     window.open('/laravel-filemanager?type=Images&addImageFunction=true','','width=800,height=600,toolbar=0,menubar=0,location=0,status=1,scrollbars=1,resizable=1,left=0,top=0');
        // });

        // var images;
        // if(images = JSON.parse($('#hidden-image-list').val())) {

        //         console.log(images);
        //     $.each(images, function(index, value) {
        //         addImage(value.filename);
        //     });
        // }

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