@extends('layouts.backend')

@section('title', 'Delete '.$theme->name)

@section('content')
    {!! Form::open(['method' => 'delete', 'route' => ['backend.themes.destroy', $theme->id]]) !!}
        <div class="alert alert-danger">
            <strong>Opgepast!</strong> Je gaat dit thema verwijderen. Deze actie kan niet ongedaan worden. Ben je zeker dat je wil verder gaan?
        </div>

        {!! Form::submit('Ja, verwijder dit thema!', ['class' => 'btn btn-danger']) !!}

        <a href="{{ route('backend.users.index') }}" class="btn btn-success">
            <strong>Nee, ga terug!</strong>
        </a>
    {!! Form::close() !!}
@endsection