<?php

namespace App\Http\Controllers\Frontend;

use Illuminate\Http\Request;
use App\Project;
use App\Opinion;
use App\Http\Requests;
use Carbon\Carbon;

class ProjectsController extends Controller
{
	protected $projects;
    protected $themes;
    protected $opinions;

	public function __construct(Project $projects, Opinion $opinions) {

		$this->projects = $projects;
        $this->opinions = $opinions;

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

        return view('frontend.projects.index', compact('projects'));
    }

    public function map() {
        $projects = $this->projects->with('theme', 'creator')->get();

        return view('frontend.projects.map', compact('projects'));
    }

    public function info($id, Opinion $opinion)
    {
        $project = $this->projects->with('theme', 'creator', 'opinions', 'stages', 'images')->findOrFail($id);
        return view('frontend.projects.info', compact('project', 'opinion'));
    }

    public function opinionstore(Requests\StoreOpinionRequest $request, $id)
    {
        $this->opinions->create(
            ['user_id' =>  auth()->check() ? auth()->user()->id : '0', 'project_id' => $id] + $request->only('opinion')
        );
        return redirect(route('frontend.projects.info', $id))->with('stats', 'Uw reactie is gelukt!');
    }

    public function opiniondestroy(Requests\DeleteOpinionRequest $request, Project $project, Opinion $opinion)
    {
        $opinion->delete();

        return redirect(route('frontend.projects.info', $project->id))->with('status', 'De reactie is verwijderd.');
    }

}