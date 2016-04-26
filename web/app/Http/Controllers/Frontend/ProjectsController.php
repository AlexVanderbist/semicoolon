<?php

namespace App\Http\Controllers\Frontend;

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

        return view('app.projects.index', compact('projects'));
    }

}
