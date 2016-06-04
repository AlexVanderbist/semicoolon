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

    public function project()
    {
        return $this->belongsTo(Project::class);
    }

    public function opinions() {
        return $this->hasMany(ProposalOpinion::class);
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
