<?php

namespace App\Http\Controllers\API;

use Illuminate\Http\Request;

use App\Http\Requests;
use App\Http\Controllers\Controller;

use App\Project;

class ProjectsController extends Controller
{
	protected $projects;

	public function __construct(Project $projects) {

		$this->projects = $projects;

	}

    public function index() {

    	$projects = $this->projects->with('theme', 'creator', 'proposals')->get();

    	return response()->json(compact('projects'));
    }
}
