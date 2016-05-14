@extends('layouts.backend')

@section('title', 'Fase voor ' . $project->name)
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
        <div class="panel-heading">{!! $stage->exists ? 'Wijzig ' . $stage->name : 'Nieuwe fase toevoegen' !!}</div>

        <div class="panel-body">
            {!! Form::model($stage, [
                'method' => $stage->exists ? 'put' : 'post',
                'route' => $stage->exists ? ['backend.projects.{project}.stages.update', $project->id, $stage->id] : ['backend.projects.{project}.stages.store', $project->id],
            ]) !!}

            <div class="form-group">
                {!! Form::label('name','Titel') !!}
                {!! Form::text('name', null, ['class' => 'form-control', 'placeholder' => 'Geef de titel van de fase...']) !!}
            </div>

            <div class="form-group">
                {!! Form::label('description','Beschrijving') !!}
                {!! Form::textarea('description', null, ['class' => 'form-control', 'placeholder' => 'Beschrijf hier uw fase...', 'rows' => 3]) !!}
            </div>
            
            <div class="form-inline">
                <div class="form-group">
                    {!! Form::label('startdate','Begindatum') !!}
                    {!! Form::date('startdate', \Carbon\Carbon::now(), ['class' => 'form-control']) !!}
                </div>
                <div class="form-group">
                    {!! Form::label('enddate','Einddatum') !!}
                    {!! Form::date('enddate', \Carbon\Carbon::now(), ['class' => 'form-control']) !!}
                </div>
            </div>
            <div class="form-inline">
                <div class="form-group">
                    {!! Form::label('allow_input','Input goedkeuren') !!}
                    {!! Form::select('allow_input', array('0' => 'Nee', '1' => 'Ja'), null, ['class' => 'form-control']) !!}
                </div>

                {!! Form::submit($stage->exists ? 'Fase wijzigen' : 'Fase toevoegen', ['class' => 'btn btn-primary pull-right']) !!}
            </div>

            {!! Form::close() !!}
        </div>

    </div>

    <table class="table table-hover" id="stages">

        <thead>
            <tr>
                <th>Fase</th>
                <th>Beschrijving</th>
                <th>Startdatum</th>
                <th>Einddatum</th>
                <th>Aanpassen</th>
                <th>Verwijderen</th>
            </tr>
        </thead>

        <tbody>

            @if($stages->isEmpty())
                <tr>
                    <td colspan="4" align="center">Er zijn nog geen fases toegevoegd.</td>
                </tr>
            @else
                @foreach($stages as $stage)
                    <tr>
                        <td>{{$stage->name}}</td>
                        <td>{{$stage->description}}</td>
                        <td>{{$stage->startdate->toFormattedDateString()}}</td>
                        <td>{{$stage->enddate->toFormattedDateString()}}</td>
                        <td>
                            <a href="{{ route('backend.projects.{project}.stages.edit', [$project->id, $stage->id]) }}">
                                <span class="glyphicon glyphicon-edit"></span>
                            </a>
                        </td>
                        <td>
                            {!! Form::open(['method' => 'delete', 'route' => ['backend.projects.{project}.stages.destroy', $project->id, $stage->id]]) !!}

                                <button type="submit" class="btn btn-danger">
                                    <span class="glyphicon glyphicon-remove"></span> Delete
                                </button>

                            {!! Form::close() !!}
                        </td>
                    </tr>
                @endforeach
            @endif
        </tbody>
    </table>

@endsection