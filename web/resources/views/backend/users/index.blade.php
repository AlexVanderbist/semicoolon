@extends('layouts.backend')

@section('title', 'Gebruikers')

@section('content')
    <a href="{{ route('backend.users.create') }}" class="btn btn-primary"><i class="fa fa-plus" aria-hidden="true"></i> Nieuwe gebruiker maken</a>

    <h4>Beheerders</h4>
    <table class="table table-hover">
        <thead>
            <tr>
                <th>Voornaam</th>
                <th>Achternaam</th>
                <th>E-mail</th>
                <th>Geboortejaar</th>
                <th>Geslacht</th>
                <th>Gemeente</th>
                <th>Aanpassen</th>
                <th>Verwijderen</th>
            </tr>
        </thead>
        <tbody>
            @foreach($admins as $admin)
                <tr>
                    <td>{{$admin->firstname}}</td>
                    <td>{{$admin->lastname}}</td>
                    <td>{{$admin->email}}</td>
                    <td>{{$admin->birthyear}}</td>
                    <td>
                        @if ($admin->sex === 0)
                            Vrouw
                        @elseif ($admin->sex === 1)
                            Man
                        @endif
                    </td>
                    <td>{{$admin->city}}</td>
                    <td>
                        <a href="{{ route('backend.users.edit', $admin->id) }}">
                            <span class="glyphicon glyphicon-edit"></span>
                        </a>
                    </td>
                    <td>
                        <a href="{{ route('backend.users.confirm', $admin->id) }}">
                            <span class="glyphicon glyphicon-remove"></span>
                        </a>
                    </td>
                </tr>
            @endforeach
        </tbody>
    </table>
    <h4>Gebruikers</h4>
    <table class="table table-hover">
        <thead>
            <tr>
                <th>Voornaam</th>
                <th>Achternaam</th>
                <th>E-mail</th>
                <th>Geboortejaar</th>
                <th>Geslacht</th>
                <th>Gemeente</th>
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
