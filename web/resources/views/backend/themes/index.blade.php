@extends('layouts.backend')

@section('title', 'Themas')

@section('content')
    <a href="{{ route('backend.themes.create') }}" class="btn btn-primary"><i class="fa fa-plus" aria-hidden="true"></i> Nieuw thema maken</a>

    <table class="table table-hover">
        <thead>
            <tr>
                <th>Thema</th>
                <th>Kleur</th>
                <th>Aanpassen</th>
                <th>Verwijderen</th>
            </tr>
        </thead>
        <tbody>
            @foreach($themes as $theme)
                <tr>
                    <td>{{$theme->name}}</td>
                    <td><div style="background-color:{{$theme->hex_color}}; height:20px; width:50%;"></div></td>
                    <td>
                        <a href="{{ route('backend.themes.edit', $theme->id) }}">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                    </td>
                    <td>
                        <a href="{{ route('backend.themes.confirm', $theme->id) }}">
                            <span class="glyphicon glyphicon-remove"></span>
                        </a>
                    </td>
                </tr>
            @endforeach
        </tbody>
    </table>

    {!! $themes->render() !!}
@endsection
