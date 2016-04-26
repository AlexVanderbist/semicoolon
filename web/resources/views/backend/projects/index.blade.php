@extends('layouts.backend')

@section('title', 'Projecten')

@section('content')
    <a href="{{ route('backend.projects.create') }}" class="btn btn-primary">Nieuw project maken</a>

    <table class="table table-hover">
        <thead>
            <tr>
                <th>Project</th>
                <th>Map</th>
                <th>Locatie</th>
                <th>Stage</th>
                <th>Thema</th>
                <th>Gemaakt door</th>
                <th>Edit</th>
                <th>Delete</th>
            </tr>
        </thead>
        <tbody>
            @foreach($projects as $project)
                <tr>
                    <td>{{$project->name}}</td>
                    <td></td>
                    <td>{{$project->locationText}}</td>
                    <td>{{$project->stage_id}}</td>
                    <td>{{$project->thema_id}}</td>
                    <td>{{$project->city}}</td>
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