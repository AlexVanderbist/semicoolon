@extends('layouts.backend')

@section('title', $user->exists ? 'Wijzig '.$user->name : 'Nieuwe gebruiker aanmaken')

@section('content')
    {!! Form::model($user, [
        'method' => $user->exists ? 'put' : 'post',
        'route' => $user->exists ? ['backend.users.update', $user->id] : ['backend.users.store']
    ]) !!}

    <div class="form-group">
        {!! Form::label('Voornaam') !!}
        {!! Form::text('firstname', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Achternaam') !!}
        {!! Form::text('lastname', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('email') !!}
        {!! Form::text('email', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('type') !!}
        {!! Form::select('admin', array('0' => 'Gebruiker', '1' => 'Admin'), $user->admin, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Geslacht') !!}
        {!! Form::select('sex', array('0' => 'Vrouw', '1' => 'Man'), $user->sex, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Geboortejaar') !!}
        {!! Form::number('birthyear', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Gemeente') !!}
        {!! Form::text('city', null, ['class' => 'form-control']) !!}
    </div>


    <div class="form-group">
        {!! Form::label('Wachtwoord') !!}
        <small>Of leeglaten om huidig wachtwoord te behouden.</small>
        {!! Form::password('password', ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Wachtwoord_bevestiging') !!}
        {!! Form::password('password_confirmation', ['class' => 'form-control']) !!}
    </div>

    {!! Form::submit($user->exists ? 'Gebruiker opslaan' : 'Nieuwe gebruiker maken', ['class' => 'btn btn-primary']) !!}
    <a href="{{ route('backend.users.index') }}" class="">of ga terug naar alle gebruikers.</a>

    {!! Form::close() !!}
@endsection