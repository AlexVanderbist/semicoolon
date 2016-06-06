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

	@foreach($proposals->chunk(3) as $proposalsChunk)
    <div class="row proposals">
        @foreach($proposalsChunk as $proposal)
            <div class="col-md-4">
                <div class="well">
					<a class="pull-right"
						data-toggle="tooltip"
						title="Verwijderen"
						href="{!! route('backend.projects.{project}.proposals.destroy', [$project->id, $proposal->id]) !!}"
						data-method="delete" data-token="{{csrf_token()}}"
						>
	                    <span class="glyphicon glyphicon-remove"></span>
					</a>
	                <h1>{{$proposal->description}}</h1>
	                <p>Aantal antwoorden: {{$proposal->num_opinions}}</p>
	                @if($proposal->type == 1 && $proposal->opinions()->count() > 0)
	                    <canvas id="Chart{{$proposal->id}}" width="200" height="200"></canvas>
	                    <script>
	                        $(function() {
	                            var ctx = document.getElementById("Chart{{$proposal->id}}");
	                            new Chart(ctx,{
	                                type: 'pie',
	                                data: {
			                            labels: [
			                                "Ja",
			                                "Nee"
			                            ],
			                            datasets: [
			                                {
			                                    data: [
													{{$proposal->vote()['yes']}},
													{{$proposal->vote()['no']}}
												],
			                                    backgroundColor: [
			                                        "rgba(99, 255, 134, 0.2)",
			                                        "rgba(255,99,132,0.2)"
			                                    ],
									            borderColor: "rgba(255,99,132,1)",
									            borderWidth: 1,
									            hoverBackgroundColor:[
			                                        "rgba(99, 255, 134, 0.7)",
			                                        "rgba(255,99,132,0.7)"
			                                    ],
									            hoverBorderColor: "rgba(255,99,132,1)"
			                                }]
			                        },
	                                options: {
								        legend: {
								            display: true,
											position: 'bottom'
								        }
									}
	                            });
	                        });
	                    </script>
	                @elseif($proposal->type == 2 && $proposal->opinions()->count() > 0)
	                    <canvas id="Chart{{$proposal->id}}" width="200" height="200"></canvas>
	                    <script>
	                        $(function() {
	                            var ctx = document.getElementById("Chart{{$proposal->id}}");
	                            new Chart(ctx,{
	                                type: 'bar',
	                                data: {
			                            labels: [
			                                "1",
			                                "2",
			                                "3",
			                                "4",
			                                "5"
			                            ],
			                            datasets: [
			                                {
            									label: "aantal stemmen",
			                                    data: [
													{{$proposal->vote()['1']}},
													{{$proposal->vote()['2']}},
													{{$proposal->vote()['3']}},
													{{$proposal->vote()['4']}},
													{{$proposal->vote()['5']}}
												],
									            backgroundColor: [
			                                        "rgba(255,99,132,0.2)",
			                                        "rgba(255, 151, 99, 0.2)",
			                                        "rgba(255, 235, 99, 0.2)",
			                                        "rgba(205, 255, 99, 0.2)",
				                                    "rgba(99, 255, 134, 0.2)"
			                                    ],
									            borderColor: "rgba(255,99,132,1)",
									            borderWidth: 1,
									            hoverBackgroundColor: [
			                                        "rgba(255,99,132,0.7)",
			                                        "rgba(255, 151, 99, 0.7)",
			                                        "rgba(255, 235, 99, 0.7)",
			                                        "rgba(205, 255, 99, 0.7)",
				                                    "rgba(99, 255, 134, 0.7)"
			                                    ],
									            hoverBorderColor: "rgba(255,99,132,1)"
			                                }]
			                        },
									options: {
								        scales: {
								            yAxes: [{
								                ticks: {
								                    beginAtZero:true
								                }
								            }]
								        },
								        legend: {
								            display: true,
											position: 'bottom'
								        }
									}
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
    @endforeach
    <a href="{{ route('backend.projects.{project}.proposals.export', $project->id) }}" class="marginbtn btn btn-primary"><i class="fa fa-file-excel-o" aria-hidden="true"></i> Exporteren</a>
@endsection
