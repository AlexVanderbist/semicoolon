<?php

namespace App\Http\Controllers\Frontend;

use Illuminate\Http\Request;
use App\Project;
use App\Opinion;
use App\Http\Requests;

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

    public function info(Opinion $opinion, $id)
    {
        $project = $this->projects->with('theme', 'creator')->findOrFail($id);
        
        return view('frontend.projects.info', compact('project', 'opinion'));
    }

    public function opinionstore(Requests\StoreOpinionRequest $request, $id)
    {
        $this->opinions->create(
            ['user_id' => auth()->user()->id, 'project_id' => $id] + $request->only('opinion')
        );
        return redirect(route('frontend.projects.info', $id))->with('stats', 'Uw reactie is gelukt!');
    }

}