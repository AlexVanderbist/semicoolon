@extends('layouts.backend')

@section('title', 'Delete '.$project->name)

@section('content')
    {!! Form::open(['method' => 'delete', 'route' => ['backend.projects.destroy', $project->id]]) !!}
        <div class="alert alert-danger">
            <strong>Opgepast!</strong> Je gaat dit project verwijderen. Deze actie kan niet ongedaan worden. Ben je zeker dat je wil verder gaan?
        </div>

        {!! Form::submit('Ja, verwijder dit project!', ['class' => 'btn btn-danger']) !!}

        <a href="{{ route('backend.projects.index') }}" class="btn btn-success">
            <strong>Nee, ga terug!</strong>
        </a>
    {!! Form::close() !!}
@endsection