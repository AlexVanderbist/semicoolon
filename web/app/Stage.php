<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Stage extends Model
{
    protected $fillable = [
        'name', 
        'description',
        'startdate', 
        'enddate',
        'allow_input',
        'project_id'
    ];

    protected $dates = ['startdate', 'enddate'];

    public function project()
    {
        return $this->belongsTo(Project::class);
    }
}
