<?php

namespace App\Http\Controllers\API;

use Illuminate\Http\Request;

use App\Http\Requests;
use App\Http\Controllers\Controller;

use App\Project;
use App\Proposal;

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

		try {
    		$projects = $this->projects->with('theme', 'creator')->get();
		}
		catch (NotFoundHttpException $e)
		{
		    $projects = [];
		}


    	return response()->json(compact('projects'));
    }

    public function getProposals($id) {
		$project = $this->projects->find($id);
		if($project) {
   			$proposals = $project->proposals;
		} else $proposals = [];

        return response()->json(compact('proposals'));
    }

    public function getProposalsForUser($projectId) {
    	$proposals = $this->proposals->where('project_id', $projectId)->has('opinions', '<', 1)->orWhereHas('opinions', function($q)
	        {
	            $q->where('user_id', '!=', \Auth::user()->id);
	        })->get();

        return response()->json(compact('proposals'));
    }
}
