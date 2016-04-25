@extends('layouts.backend')

@section('title', $theme->exists ? 'Wijzig '.$theme->name : 'Nieuw thema aanmaken')

@section('content')
    {!! Form::model($theme, [
        'method' => $theme->exists ? 'put' : 'post',
        'route' => $theme->exists ? ['backend.themes.update', $theme->id] : ['backend.themes.store']
    ]) !!}

    <div class="form-group">
        {!! Form::label('Naam') !!}
        {!! Form::text('name', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Kleur') !!}
        {!! Form::input('color', 'hex_color', null, array('class' => 'form-control')) !!}
    </div>

    {!! Form::submit($theme->exists ? 'Thema opslaan' : 'Nieuw thema maken', ['class' => 'btn btn-primary']) !!}
    <a href="{{ route('backend.themes.index') }}" class="">of ga terug naar alle themas.</a>

    {!! Form::close() !!}
@endsection