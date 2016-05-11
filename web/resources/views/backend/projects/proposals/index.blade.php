@extends('layouts.backend')

@section('title', 'Stellingen voor ' . $project->name)

@section('content')
    <a href="{{ route('backend.projects.index') }}" class="btn btn-default">
        <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span> Overzicht projecten
    </a>
    <a href="{{ route('backend.projects.index') }}" class="btn btn-default">
        <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Project aanpassen
    </a>

    <div class="panel panel-default">
        <!-- List group -->
        <ul class="list-group">
        <li class="list-group-item">Cras justo odio</li>
        <li class="list-group-item">Dapibus ac facilisis in</li>
        <li class="list-group-item">Morbi leo risus</li>
        <li class="list-group-item">Porta ac consectetur ac</li>
        <li class="list-group-item">Vestibulum at eros</li>
        </ul>

        <div class="panel-footer">
            <p>...</p>
        </div>
    </div>

    @foreach($proposals as $proposal)
        <div></div>
    @endforeach

@endsection