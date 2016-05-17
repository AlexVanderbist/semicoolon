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
            'avg' => $this->opinions()->ofType(2)->avg('value')
        ];
    }
}
