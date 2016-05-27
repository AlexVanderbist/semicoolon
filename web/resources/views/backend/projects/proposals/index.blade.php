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

    <div class="row proposals">
        @foreach($proposals as $proposal)
            <div class="col-md-4">
                <div class="well">
                <h1>{{$proposal->description}} <a class="pull-right" data-toggle="tooltip" title="Verwijderen" href="{!! route('backend.projects.{project}.proposals.destroy', [$project->id, $proposal->id]) !!}" data-method="delete" data-token="{{csrf_token()}}">
                                <span class="glyphicon glyphicon-remove"></span></a></h1>
                <p>Aantal antwoorden: {{$proposal->opinions()->count()}}</p>
                @if($proposal->type == 1 && $proposal->opinions()->count() > 0)
                    <canvas id="Chart{{$proposal->id}}" width="200" height="200"></canvas>
                    <script>
                        var data{{$proposal->id}} = {
                            labels: [
                                "Ja",
                                "Nee"
                            ],
                            datasets: [
                                {
                                    data: [{{$proposal->vote()['yes']}}, {{$proposal->vote()['no']}}],
                                    backgroundColor: [
                                        "#016F01",
                                        "#960000"
                                    ],
                                    hoverBackgroundColor: [
                                        "#009600",
                                        "#CA0000"
                                    ]
                                }]
                        };
                        $(function() {    
                            var ctx = $("#Chart{{$proposal->id}}").get(0).getContext("2d");
                            var myPieChart = new Chart(ctx,{
                                type: 'pie',
                                data: data{{$proposal->id}},
                                options: chartoptions
                            });
                        });
                    </script>
                @elseif($proposal->type == 2 && $proposal->opinions()->count() > 0)
                    <canvas id="Chart{{$proposal->id}}" width="200" height="200"></canvas>
                    <script>
                        var data{{$proposal->id}} = {
                            labels: [
                                "Éen",
                                "Twee",
                                "Drie",
                                "Vier",
                                "Vijf"
                            ],
                            datasets: [
                                {
                                    data: [{{$proposal->vote()['1']}}, {{$proposal->vote()['2']}}, {{$proposal->vote()['3']}}, {{$proposal->vote()['4']}}, {{$proposal->vote()['5']}}],
                                    backgroundColor: [
                                        "#960000",
                                        "#CC6900",
                                        "#B1B500",
                                        "#27A500",
                                        "#00E614"
                                    ],
                                    hoverBackgroundColor: [
                                        "#009600",
                                        "##F17C00",
                                        "#CDDC00",
                                        "#2CBB00",
                                        "#06FF1B"
                                    ]
                                }]
                        };
                        $(function() {    
                            var ctx = $("#Chart{{$proposal->id}}").get(0).getContext("2d");
                            var myPieChart = new Chart(ctx,{
                                type: 'pie',
                                data: data{{$proposal->id}},
                                options: chartoptions
                            });
                        });
                    </script>
                @else
                    <p>Er zijn nog geen antwoorden gegeven</p>
                @endif
                
                
                <a href="{!! route('backend.projects.{project}.proposals.opinions.destroy', [$project->id, $proposal->id]) !!}" data-method="delete" data-token="{{csrf_token()}}">
                    <p>Reset</p>
                </a>
                </div>
            </div> 
        @endforeach
    </div>
@endsection