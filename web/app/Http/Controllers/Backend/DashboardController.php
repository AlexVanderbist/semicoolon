<?php

namespace App\Http\Controllers\Backend;

use App\Http\Requests;
use Illuminate\Http\Request;
use App\User;
use App\Project;
use App\ProposalOpinion;
use App\Opinion;
use App\Proposal;

class DashboardController extends Controller
{
	protected $users;
	protected $projects;
	protected $proposalopinions;
	protected $opinions;
	protected $proposals;
	public function __construct(Proposal $proposals, User $users, Project $projects, ProposalOpinion $proposalopinions, Opinion $opinions) {
		$this->users = $users;
		$this->projects = $projects;
		$this->proposalopinions = $proposalopinions;
		$this->opinions = $opinions;
		$this->proposals = $proposals;
		parent::__construct();
	}

    public function index() {
    	$users = $this->users->get();
    	$projects = $this->projects->count();
    	$proposalopinions = $this->proposalopinions->count();
    	$opinions = $this->opinions->count();
    	$proposals = $this->proposals->count();
        return view('backend.dashboard', compact('users', 'user', 'projects', 'proposalopinions', 'opinions', 'proposals'));
    }

}
