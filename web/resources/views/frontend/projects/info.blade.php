@extends('layouts.frontend')

@section('title', $project->name)

@section('content')
    <div class="container">
        <p>{{$project->thema_id}}</p>
        <h1>map</h1>
        <p>{{$project->locationText}}</p>
    </div>
@endsection