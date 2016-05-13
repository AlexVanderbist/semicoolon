@extends('layouts.backend')

@section('title', 'Projecten')

@section('content')
    <a href="{{ route('backend.projects.create') }}" class="btn btn-primary">Nieuw project maken</a>

    <table class="table table-hover">
        <thead>
            <tr>
                <th>Project</th>
                <th>Locatie</th>
                <th>Thema</th>
                <th>Gemaakt door</th>
                <th>Stellingen</th>
                <th>Edit</th>
                <th>Delete</th>
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
                        <a href="{{ route('backend.projects.{project}.proposals.index', $project->id) }}">
                            <span class="glyphicon glyphicon-pencil"></span>
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