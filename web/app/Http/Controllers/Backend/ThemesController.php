<?php

namespace App\Http\Controllers\Backend;

use Illuminate\Http\Request;
use App\Theme;
use App\Http\Requests;

class ThemesController extends Controller
{
	protected $themes;

	public function __construct(Theme $themes) {
		$this->themes = $themes;

		parent::__construct();
	}

    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        $themes = $this->themes->paginate(10);

        return view('backend.themes.index', compact('themes'));
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create(Theme $theme)
    {
        return view('backend.themes.form', compact('theme'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Requests\StoreThemeRequest $request)
    {
        $this->themes->create($request->only('name', 'hex_color'));

        return redirect(route('backend.themes.index'))->with('stats', 'Het thema is gemaakt!');
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        $themes = $this->themes->findOrFail($id);

        return view('backend.themes.form', compact('theme'));
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Requests\UpdateThemeRequest $request, $id)
    {
        $themes = $this->themes->findOrFail($id);

        $theme->fill($request->only('name', 'hex_color'))->save();

        return redirect(route('backend.themes.edit', $theme->id))->with('status', 'Het thema is geupdate!');
    }

    public function confirm(Requests\DeleteThemeRequest $request, $id)
    {
        $theme = $this->themes->findOrFail($id);

        return view('backend.themes.confirm', compact('theme'));
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy(Requests\DeleteThemeRequest $request, $id)
    {
        $theme = $this->themes->findOrFail($id);

        $theme->delete();

        return redirect(route('backend.themes.index'))->with('status', 'Het thema is verwijderd.');
    }
}
