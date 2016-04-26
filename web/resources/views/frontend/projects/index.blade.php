@extends('layouts.app')

@section('title', 'Projecten')

@section('content')
<div class="container">    
    <table class="table table-hover">
        <thead>
            <tr>
                <th>Project</th>
                <th>Map</th>
                <th>Locatie</th>
                <th>Stage</th>
                <th>Thema</th>
            </tr>
        </thead>
        <tbody>
            @foreach($projects as $project)
                <tr>
                    <td><a href="{{ route('frontend.projects.edit', $project->id) }}">{{$project->name}}</a></td>
                    <td></td>
                    <td>{{$project->locationText}}</td>
                    <td>{{$project->stage_id}}</td>
                    <td>{{$project->thema_id}}</td>
                </tr>
            @endforeach
        </tbody>
    </table>
</div>

    {!! $projects->render() !!}
@endsection