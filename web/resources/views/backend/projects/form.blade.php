@extends('layouts.backend')

@section('title', $project->exists ? 'Wijzig '.$project->name : 'Nieuw project aanmaken')

@section('content')
    {!! Form::model($project, [
        'method' => $project->exists ? 'put' : 'post',
        'route' => $project->exists ? ['backend.projects.update', $project->id] : ['backend.projects.store']
    ]) !!}

    <div class="form-group">
        {!! Form::label('Naam') !!}
        {!! Form::text('name', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('MAP') !!}
        {!! Form::text('lat', null, ['class' => 'form-control']) !!}
        {!! Form::text('lng', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Locatie') !!}
        {!! Form::text('locationText', null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Stage') !!}
        {!! Form::select('stage_id', array('0' => '0', '1' => '1'), $project->stage, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Thema') !!}
        {!! Form::select('thema_id', [
                '' => ''
            ] + $getThemes->lists('name', 'id')->toArray(), null, ['class' => 'form-control']) !!}
    </div>

    <div class="form-group">
        {!! Form::label('Gemaakt door') !!}
        {!! Form::text('project_creator', null, ['class' => 'form-control']) !!}
    </div>

    {!! Form::submit($project->exists ? 'Project opslaan' : 'Nieuw project maken', ['class' => 'btn btn-primary']) !!}
    <a href="{{ route('backend.projects.index') }}" class="">of ga terug naar alle projecten.</a>

    {!! Form::close() !!}
@endsection