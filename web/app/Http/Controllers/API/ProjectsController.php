<?php

namespace App\Http\Controllers\API;

use Illuminate\Http\Request;

use App\Http\Requests;
use App\Http\Controllers\Controller;

use App\Project;
use App\Proposal;
use App\Theme;

class ProjectsController extends Controller
{
	protected $projects;
	protected $proposals;

	public function __construct(Project $projects, Proposal $proposals) {

		$this->projects = $projects;
		$this->proposals = $proposals;
	}

    public function index() 
    {
		$projects = $this->projects->with('theme', 'creator', 'images', 'stages')->get();
	    //$projects = [];

    	return response()->json(compact('projects'));
    }

    public function getThemes(Theme $themes) 
    {
    	$themes = $themes->all();
    	return response()->json(compact('themes'));
    }
}
