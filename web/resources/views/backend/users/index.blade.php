@extends('layouts.backend')

@section('title', 'Gebruikers')

@section('content')
    <a href="{{ route('backend.users.create') }}" class="btn btn-primary">Nieuwe gebruiker maken</a>

    <table class="table table-hover">
        <thead>
            <tr>
                <th>Voornaam</th>
                <th>Achternaam</th>
                <th>E-mail</th>
                <th>Geboortejaar</th>
                <th>Geslacht</th>
                <th>Gemeente</th>
                <th>Type</th>
                <th>Aanpassen</th>
                <th>Verwijderen</th>
            </tr>
        </thead>
        <tbody>
            @foreach($users as $user)
                <tr>
                    <td>{{$user->firstname}}</td>
                    <td>{{$user->lastname}}</td>
                    <td>{{$user->email}}</td>
                    <td>{{$user->birthyear}}</td>
                    <td>
                        @if ($user->sex === 0)
                            Vrouw
                        @elseif ($user->sex === 1) 
                            Man
                        @endif
                    </td>
                    <td>{{$user->city}}</td>
                    <td>
                        @if ($user->admin === 0)
                            Gebruiker
                        @elseif ($user->admin === 1) 
                            Admin
                        @endif
                    </td>
                    <td>
                        <a href="{{ route('backend.users.edit', $user->id) }}">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                    </td>
                    <td>
                        <a href="{{ route('backend.users.confirm', $user->id) }}">
                            <span class="glyphicon glyphicon-remove"></span>
                        </a>
                    </td>
                </tr>
            @endforeach
        </tbody>
    </table>

    {!! $users->render() !!}
@endsection