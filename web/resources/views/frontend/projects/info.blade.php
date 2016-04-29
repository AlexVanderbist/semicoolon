@extends('layouts.app')

@section('title', $project->name)

@section('content')

    <div class="container">
        <h1>{{$project->name}}</h1>
        <h3>{{$project->locationText}}</h3>
        <p>door {{$project->creator->full_name}} op {{$project->created_at}}</p>
        <p>{!! $project->descipription !!}</p>
    </div>
@endsection