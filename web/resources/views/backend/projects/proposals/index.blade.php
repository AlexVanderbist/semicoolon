@extends('layouts.backend')

@section('title', 'Stellingen voor ' . $project->name)

@section('content')
    <div class="btn-group" role="group" style="margin-bottom:15px">
        <a href="{{ route('backend.projects.index') }}" class="btn btn-default">
            <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span> Overzicht projecten
        </a>
        <a href="{{ route('backend.projects.edit', $project->id) }}" class="btn btn-default">
            <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span> Project aanpassen
        </a>
    </div>

    <div class="panel panel-default">

        <div class="panel-body">
            <p>Nieuwe stelling toevoegen</p>
        </div>

        <table class="table table-hover">
            <tbody>
                @foreach($proposals as $proposal)
                    <tr>
                        <td>{{$proposal->description}}</td>
                        <td>{{$proposal->type}}</td>
                        <td>
                            ja: 5 | nee: 8
                        </td>
                    </tr>
                @endforeach
            </tbody>
        </table>
    </div>

@endsection