<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class Opinion extends Model
{
    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'user_id', 
        'project_id',
        'anon_user_id',
        'opinion',
    ];
    
    public function posted_by()
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}
