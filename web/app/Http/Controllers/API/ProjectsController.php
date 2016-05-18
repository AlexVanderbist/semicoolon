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

    	$projects = $this->projects->with('theme', 'creator')->get();

    	return response()->json(compact('projects'));
    }

    public function getProposals(Project $project) {
        $proposals = $project->proposals;
        return response()->json(compact('proposals'));
    }

    public function getProposalsForUser(Project $project) {
        $proposals = $project->with('proposals')->whereHas('proposals.opinions', function($q)
        {
            $q->where('user_id', '!=', \Auth::user()->id);
        });

        return response()->json(compact('proposals'));
    }
}
