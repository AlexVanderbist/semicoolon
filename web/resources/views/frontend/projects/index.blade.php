@extends('layouts.app')

@section('title', 'Projecten')

@section('content')
<div class="container">    
    <table class="table table-hover">
        <thead>
            <tr>
                <th>Project</th>
                <th>Locatie</th>
                <th>Thema</th>
            </tr>
        </thead>
        <tbody>
            @foreach($projects as $project)
                <tr>
                    <td><a href="{{ route('frontend.projects.info', $project->id) }}">{{$project->name}}</a></td>
                    <td>{{$project->locationText}}</td>
                    <td>{{$project->theme_id}}</td>
                </tr>
            @endforeach
        </tbody>
    </table>
</div>

    {!! $projects->render() !!}
@endsection