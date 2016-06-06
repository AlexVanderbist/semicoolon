<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Proposal extends Model
{

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'description',
        'type',
        'project_id'
    ];

	protected $appends = ['stats_string', 'num_opinions'];

    public function project()
    {
        return $this->belongsTo(Project::class);
    }

    public function opinions() {
        return $this->hasMany(ProposalOpinion::class);
    }

	public function getStatsStringAttribute() {
		$votes = $this->vote();
        if ($this->getNumOpinionsAttribute() > 0) {
    		switch ($this->attributes['type']) {
    			case 1:
    				# Yes no=...
    				return "Ja: " + $votes['yes'] + " | Nee: " + $votes['no'];
    				break;

    			case 2:
    				# 1-5...
    				return "1: " + $votes['1'] + " | 2: " + $votes['2'] + " | 3: " + $votes['3'] + " | 4: " + $votes['4'] + " | 5: " + $votes['5'];
    				break;
    		}
        }
        else {
            return "Geen antwoorden gegeven";
        }
	}

	public function getNumOpinionsAttribute() {
		return $this->opinions()->count();
	}

    public function vote() {
        //dd($this->opinions()->ofType(1)->get());

        return [
            'yes' => $this->opinions()->ofType(1)->withValue(1)->count(),
            'no'  => $this->opinions()->ofType(1)->withValue(2)->count(),
            'avg' => $this->opinions()->ofType(2)->avg('value'),
            '1'  => $this->opinions()->ofType(2)->withValue(1)->count(),
            '2'  => $this->opinions()->ofType(2)->withValue(2)->count(),
            '3'  => $this->opinions()->ofType(2)->withValue(3)->count(),
            '4'  => $this->opinions()->ofType(2)->withValue(4)->count(),
            '5'  => $this->opinions()->ofType(2)->withValue(5)->count()
        ];
    }
    public function statistics() {
        $all =  $this->opinions()->count();
        $yes = $this->opinions()->ofType(1)->withValue(1)->count();
        if($all>0)
        {
            $statistic = ($yes/$all)*100;
        }
        else
        {
            $statistic = 0;
        }
        return $statistic;
    }
}
