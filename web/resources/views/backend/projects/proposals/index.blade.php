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
        <div class="panel-heading">Nieuwe stelling toevoegen</div>

        <div class="panel-body">
            {!! Form::model($proposal, [
                'method' => 'post',
                'route' => ['backend.projects.{project}.proposals.store', $project->id],
            ]) !!}


            <div class="form-group">
                {!! Form::label('description','De stelling') !!}
                {!! Form::textarea('description', null, ['class' => 'form-control', 'placeholder' => 'Typ hier uw stelling...', 'rows' => 3]) !!}
            </div>

            <div class="form-inline">
                <div class="form-group">
                    {!! Form::select('type', array('1' => 'Ja/nee vraag', '2' => 'Score 1-5'), null, ['class' => 'form-control']) !!}
                </div>

                {!! Form::submit('Nieuwe stelling toevoegen', ['class' => 'btn btn-primary pull-right']) !!}
            </div>

            {!! Form::close() !!}
        </div>

    </div>

    <table class="table table-hover">

        <thead>
            <tr>
                <th>Stelling</th>
                <th>Type</th>
                <th>Stemming</th>
                <th>Verwijderen</th>
            </tr>
        </thead>

        <tbody>

            @if($proposals->isEmpty())
                <tr>
                    <td colspan="4" align="center">Er zijn nog geen stellingen toegevoegd.</td>
                </tr>
            @else
                @foreach($proposals as $proposal)
                    <tr>
                        <td>{{$proposal->description}}</td>
                        <td>{{$proposal->type}}</td>
                        <td>
                            ja: 5 | nee: 8
                        </td>
                        <td>
                            {!! Form::open(['method' => 'delete', 'route' => ['backend.projects.{project}.proposals.destroy', $project->id, $proposal->id]]) !!}

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