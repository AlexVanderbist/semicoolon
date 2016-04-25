@extends('layouts.backend')

@section('title', 'Delete '.$user->name)

@section('content')
    {!! Form::open(['method' => 'delete', 'route' => ['backend.users.destroy', $user->id]]) !!}
        <div class="alert alert-danger">
            <strong>Opgepast!</strong> Je gaat deze gebruiker verwijderen. Deze actie kan niet ongedaan worden. Ben je zeker dat je wil verder gaan?
        </div>

        {!! Form::submit('Ja, verwijder deze gebruiker!', ['class' => 'btn btn-danger']) !!}

        <a href="{{ route('backend.users.index') }}" class="btn btn-success">
            <strong>Nee, ga terug!</strong>
        </a>
    {!! Form::close() !!}
@endsection