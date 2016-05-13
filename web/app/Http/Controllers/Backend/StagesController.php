<?php

namespace App\Http\Controllers\Backend;

use Illuminate\Http\Request;
use App\Project;
use App\Stage;
use App\Http\Requests;

class StagesController extends Controller
{
    protected $stages;

    public function __construct(Stage $stages) {
        $this->stages = $stages;

        parent::__construct();
    }

    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index(Project $project, Stage $stage)
    { 
        $stages = $project->stages;

        return view('backend.projects.stages.index', compact('project', 'stages', 'stage'));
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Requests\StoreProjectStageRequest $request, Project $project)
    {
        $this->stages->create($request->all() + ['project_id' => $project->id]);

        return redirect(route('backend.projects.{project}.stages.index', $project->id))->with('stats', 'De fase is toegevoegd!');
    }

    public function update(Requests\UpdateProjectStageRequest $request, Project $project, Stage $stage)
    {
        $this->stages->fill($request->all() + ['project_id' => $project->id]);

        return redirect(route('backend.projects.{project}.stages.index', $project->id))->with('stats', 'De fase is geupdate!');
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy(Project $project, $id)
    {

        $stage = $this->stages->findOrFail($id);

        $stage->delete();

        return redirect(route('backend.projects.{project}.stages.index', $project->id))->with('status', 'De fase is verwijderd.');
    }
}
