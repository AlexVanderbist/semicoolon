<?php

namespace App\Http\Controllers\Backend;

use Illuminate\Http\Request;
use App\User;
use App\Http\Requests;

class UsersController extends Controller
{
	protected $users;

	public function __construct(User $users) {
		$this->users = $users;

		parent::__construct();
	}

    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        // $users = $this->users->paginate(10);
        $users = $this->users->where('admin', 0)->paginate(10);
        $admins = $this->users->where('admin', 1)->get();

        return view('backend.users.index', compact('users','admins'));
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create(User $user)
    {
        return view('backend.users.form', compact('user'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Requests\StoreUserRequest $request)
    {
        $this->users->create($request->only('firstname', 'lastname','email', 'admin','password','sex','birthyear','city'));

        return redirect(route('backend.users.index'))->with('stats', 'De gebruiker is gemaakt!');
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        $user = $this->users->findOrFail($id);

        return view('backend.users.form', compact('user'));
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Requests\UpdateUserRequest $request, $id)
    {
        $user = $this->users->findOrFail($id);

        $user->fill($request->only('firstname', 'lastname','email', 'admin','password','sex','birthyear','city'))->save();

        return redirect(route('backend.users.edit', $user->id))->with('status', 'De gebruiker is geupdate!');
    }

    public function confirm(Requests\DeleteUserRequest $request, $id)
    {
        $user = $this->users->findOrFail($id);

        return view('backend.users.confirm', compact('user'));
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy(Requests\DeleteUserRequest $request, $id)
    {
        $user = $this->users->findOrFail($id);

        $user->delete();

        return redirect(route('backend.users.index'))->with('status', 'De gebruiker is verwijderd.');
    }
}
