<?php

namespace App\Http\Controllers\Frontend;

use Illuminate\Http\Request;
use App\Project;
use App\Http\Requests;

class ProjectsController extends Controller
{
	protected $projects;
    protected $themes;

	public function __construct(Project $projects) {

		$this->projects = $projects;

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

    public function info($id)
    {
        $project = $this->projects->with('theme', 'creator')->findOrFail($id);
        
        return view('frontend.projects.info', compact('project'));
    }

}
