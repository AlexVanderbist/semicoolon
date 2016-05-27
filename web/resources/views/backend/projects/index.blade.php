@extends('layouts.backend')

@section('title', 'Projecten')

@section('content')
    <a href="{{ route('backend.projects.create') }}" class="btn btn-primary">Nieuw project maken</a>
    
    
    @foreach($projects->chunk(3) as $projectChunk)
        <div class="row">
            @foreach($projectChunk as $project)
                <div class="col-md-4">
                    <div class="panel panel-default">
                        <div class="panel-heading projectheading"><span class="glyphicon glyphicon-bookmark" style="color:{{$project->theme->hex_color}};margin-right:5px;"></span>{{$project->name}} 
                            <a class="pull-right projectlink" data-toggle="tooltip" title="Verwijderen" href="{{ route('backend.projects.confirm', $project->id) }}">
                                <span class="glyphicon glyphicon-remove"></span>
                            </a>
                        </div>
                        @if($project->header_image)
                        <div class="projectimg">
                            <img src="{{asset($project->header_image->filename)}}" alt="{{$project->name}} ">
                        </div>
                        @endif
                        <div class="panel-body projectbody">
                            {!! $project->description !!}
                        </div>
                        <ul class="list-group">
                            <li class="list-group-item">
                                    <p><strong>Locatie:</strong> {{$project->locationText}}</p>
                                    <p><strong>Thema:</strong> {{$project->theme->name}} </p>
                                    <p><strong>Gemaakt door</strong> {{$project->creator->full_name}}</p>
                            </li>
                        </ul>
                        <div class="panel-footer">
                            <div class="row">
                                <a class="col-md-3 text-center" data-toggle="tooltip" title="Fases" href="{{ route('backend.projects.{project}.stages.index', $project->id) }}">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </a>

                                <a class="col-md-3 text-center" data-toggle="tooltip" title="Stellingen" href="{{ route('backend.projects.{project}.proposals.index', $project->id) }}">
                                    <span class="glyphicon glyphicon-pencil"></span>
                                </a>

                                <a class="col-md-3 text-center" data-toggle="tooltip" title="Foto's" href="{{ route('backend.projects.{project}.images.index', $project->id) }}">
                                    <span class="glyphicon glyphicon-picture"></span>
                                </a>

                                <a class="col-md-3 text-center" data-toggle="tooltip" title="Aanpassen" href="{{ route('backend.projects.edit', $project->id) }}">
                                    <span class="glyphicon glyphicon-edit"></span>
                                </a>  
                            </div>
                        </div>
                    </div>
                </div>
            @endforeach
        </div>
    @endforeach
    <table class="table table-hover">
        <thead>
            <tr>
                <th>Project</th>
                <th>Locatie</th>
                <th>Thema</th>
                <th>Gemaakt door</th>
                <th>Fases</th>
                <th>Stellingen</th>
                <th>Foto's</th>
                <th>Aanpassen</th>
                <th>Verwijderen</th>
            </tr>
        </thead>
        <tbody>
            @foreach($projects as $project)
                <tr>
                    <td>{{$project->name}}</td>
                    <td>{{$project->locationText}}</td>
                    <td>{{$project->theme->name}}</td>
                    <td>{{$project->creator->full_name}}</td>
                    <td>
                        <a href="{{ route('backend.projects.{project}.stages.index', $project->id) }}">
                            <span class="glyphicon glyphicon-calendar"></span>
                        </a>
                    </td>
                    <td>
                        <a href="{{ route('backend.projects.{project}.proposals.index', $project->id) }}">
                            <span class="glyphicon glyphicon-pencil"></span>
                        </a>
                    </td>
                    <td>
                        <a href="{{ route('backend.projects.{project}.images.index', $project->id) }}">
                            <span class="glyphicon glyphicon-picture"></span>
                        </a>
                    </td>
                    <td>
                        <a href="{{ route('backend.projects.edit', $project->id) }}">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                    </td>
                    <td>
                        <a href="{{ route('backend.projects.confirm', $project->id) }}">
                            <span class="glyphicon glyphicon-remove"></span>
                        </a>
                    </td>
                </tr>
            @endforeach
        </tbody>
    </table>

    {!! $projects->render() !!}
@endsection