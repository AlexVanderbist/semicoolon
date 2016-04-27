@extends('layouts.app')

@section('title', $project->name)

@section('content')
    <div class="container">
        <h1>{{$project->name}}</h1>
        <h3>{{$project->locationText}}</h3>
        <p>door {{$project->creator->full_name}} op {{$project->created_at}}</p>
        <div id="locationSelector" style="height: 400px;"></div>
    </div>

    <script>
        $('#locationSelector').locationpicker({
            location: {latitude: ({{$project->lat}} || 51.218686), longitude: {{$project->lng}} || 4.417458},
            radius: 300,
            zoom: 13,
            scrollwheel: true,
            inputBinding: {
                latitudeInput: {{$project->lat}},
                longitudeInput: {{$project->lng}},
                locationNameInput: {{$project->name}}
            },
            enableAutocomplete: true,
            enableReverseGeocode: true
        });
    </script>
@endsection