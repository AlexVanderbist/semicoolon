<?php

namespace App\Http\Controllers\Backend;

use Illuminate\Http\Request;
use App\Project;
use App\Theme;
use App\Http\Requests;

class ProjectsController extends Controller
{
	protected $projects;
    protected $themes;

	public function __construct(Project $projects, Theme $themes) {
		$this->projects = $projects;
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
        $projects = $this->projects->paginate(10);

        return view('backend.projects.index', compact('projects'));
    }

    /**
     * Show the form for creating a new resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function create(Project $project)
    {
        $getThemes = $this->themes->orderBy('id','asc')->get();
        return view('backend.projects.form', compact('project', 'getThemes'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Requests\StoreProjectRequest $request)
    {
        $this->projects->create($request->only('name', 'lat','lng', 'locationText','stage_id','thema_id','project_creator'));

        return redirect(route('backend.projects.index'))->with('stats', 'Het project is gemaakt!');
    }

    /**
     * Show the form for editing the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function edit($id)
    {
        $project = $this->projects->findOrFail($id);
        $getThemes = $this->themes->orderBy('id','asc')->get();

        return view('backend.projects.form', compact('project', 'getThemes'));
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Requests\UpdateProjectRequest $request, $id)
    {
        $project = $this->projects->findOrFail($id);

        $project->fill($request->only('name', 'lat','lng', 'locationText','stage_id','thema_id','project_creator'))->save();

        return redirect(route('backend.projects.edit', $project->id))->with('status', 'Het project is geupdate!');
    }

    public function confirm(Requests\DeleteProjectRequest $request, $id)
    {
        $project = $this->projects->findOrFail($id);

        return view('backend.projects.confirm', compact('project'));
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy(Requests\DeleteProjectRequest $request, $id)
    {
        $project = $this->projects->findOrFail($id);

        $project->delete();

        return redirect(route('backend.projects.index'))->with('status', 'Het project is verwijderd.');
    }
}
