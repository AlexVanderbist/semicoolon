<?php

namespace App\Http\Controllers\API;

use Illuminate\Http\Request;

use App\Http\Requests;
use App\Http\Controllers\Controller;

use App\Project;
use App\Proposal;

class ProposalsController extends Controller
{
	protected $projects;
	protected $proposals;

	public function __construct(Project $projects, Proposal $proposals) {

		$this->projects = $projects;
		$this->proposals = $proposals;
	}

    public function getProposals($id) {
		$project = $this->projects->find($id);
		if($project) {
   			$proposals = $project->proposals;
		} else $proposals = [];
        return response()->json(compact('proposals'));
    }

    public function getProposalsForUser($projectId) {
    	$proposals = $this->proposals->where('project_id', $projectId)
    								 ->where(function($query) {
	    								 $query
		    								 ->whereDoesntHave('opinions', function($q)
										        {
										            $q->where('user_id', '=', \Auth::user()->id);
										        })
		    								 ->orHas('opinions', '<', 1);
    								 })
    								 //dd($proposals->toSql());
    								 ->get();

        return response()->json(compact('proposals'));
    }

    public function postProposalOpinionForUser(Requests\StoreProposalOpinionRequest $request, $id) {
    	$proposal = $this->proposals->find($id);
    	if($proposal){
	    	$proposal->opinions()->where('user_id', $request->user()->id)->delete();

	    	$this->proposals->find($id)->opinions()->create([
	    		'user_id' => $request->user()->id,
	    		'value' => $request->value,
	    		'type' => $proposal->type
	    	]);

	    	$status = 'success';
    	} else $status = 'proposal not found';

        return response()->json(['status' => $status]);
    }
}
