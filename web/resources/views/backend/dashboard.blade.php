@extends('layouts.backend')

@section('title', '')

@section('content')
<div class="container">
    <div class="row">
        <div class="col-md-10 col-md-offset-1">
            <div class="panel panel-default">
                <div class="panel-heading">Dashboard</div>

                <div class="panel-body">
                    <p>Welkom <strong>{{$user->fullname}}</strong></p>
                    <div class="row text-center">
                        <div class="dashtistic col-md-4">
                            <h4>Projecten</h4>
                            <h3 id="projects">0</h3>
                        </div>
                        <div class="dashtistic col-md-4">
                            <h4>Reacties</h4>
                            <h3 id="opinions">0</h3>
                        </div>
                        <div class="dashtistic col-md-4">
                            <h4>Stellingen</h4>
                            <h3 id="proposals">0</h3>
                        </div>
                    </div>
                    <div class="row text-center">
                        <div class="dashtistic col-md-4">
                            <h4>Gebruikers</h4>
                            <h3 id="users">0</h3>
                        </div>
                        <div class="dashtistic col-md-4">
                            <h4>Geslacht</h4>
                            <div class="sexpie">
                                <canvas id="sex" width="100" height="100"></canvas>
                            </div>
                        </div>
                        <div class="dashtistic col-md-4">
                            <h4>Antwoorden</h4>
                            <h3 id="proposalopinions">0</h3>
                        </div>
                    </div>
                    <div class="row text-center">
                        <div class="col-md-4">
                            <a href="{{ route('backend.projects.create') }}" class="marginbtn btn btn-primary"><i class="fa fa-plus" aria-hidden="true"></i> Nieuw project maken</a>
                        </div>
                        <div class="col-md-4">
                            <a href="{{ route('backend.themes.create') }}" class="btn btn-primary"><i class="fa fa-plus" aria-hidden="true"></i> Nieuw thema maken</a>
                        </div>
                        <div class="col-md-4">
                            <a href="{{ route('backend.users.create') }}" class="btn btn-primary"><i class="fa fa-plus" aria-hidden="true"></i> Nieuwe gebruiker maken</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<script>
    $( document ).ready(function() {
        $('#users').animateNumber({ number: {{$users->count()}} });
        $('#projects').animateNumber({ number: {{$projects}} });
        $('#proposalopinions').animateNumber({ number: {{$proposalopinions}} });
        $('#opinions').animateNumber({ number: {{$opinions}} });
        $('#proposals').animateNumber({ number: {{$proposals}} });

        $(function() {
            var ctx = document.getElementById("sex");
            new Chart(ctx,{
                type: 'pie',
                data: {
                    labels: [
                        "Mannen",
                        "Vrouwen"
                    ],
                    datasets: [
                    {
                    data: [
                        {{$users->where('sex', 1)->get()->count()}},
                        {{$users->where('sex', 0)->get()->count()}}
                    ],
                    backgroundColor: [
                        "#1964B2",
                        "#CF0039"
                    ],
                        borderColor: "#f5f5f5",
                        borderWidth: 1,
                        hoverBackgroundColor:[
                            "#248fff",
                            "#ff0046"
                        ],
                    hoverBorderColor: "#f5f5f5"
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
    });
</script>
@endsection