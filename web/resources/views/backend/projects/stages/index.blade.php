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
                <div class="form-group pull-right">
                    {!! Form::submit($stage->exists ? 'Fase wijzigen' : 'Fase toevoegen', ['class' => 'btn btn-primary']) !!}
                </div>
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
                        <td>{{$stage->startdate->formatLocalized('%A %d %B %Y')}}</td> <!-- in normal format and Dutch -->
                        <td>{{$stage->enddate->formatLocalized('%A %d %B %Y')}}</td>
                        <td>
                            <a data-toggle="tooltip" title="Aanpassen" href="{{ route('backend.projects.{project}.stages.edit', [$project->id, $stage->id]) }}">
                                <span class="glyphicon glyphicon-edit"></span>
                            </a>
                        </td>
                        <td>
                            <a data-toggle="tooltip" title="Verwijderen" href="{!! route('backend.projects.{project}.stages.destroy', [$project->id, $stage->id]) !!}" data-method="delete" data-token="{{csrf_token()}}">
                                <span class="glyphicon glyphicon-remove"></span>
                            </a>
                        </td>
                    </tr>
                @endforeach
            @endif
        </tbody>
    </table>

@endsection