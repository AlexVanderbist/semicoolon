<?php

namespace App;

use Illuminate\Foundation\Auth\User as Authenticatable;

class User extends Authenticatable
{
    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'firstname',
        'lastname',
        'email',
        'admin',
        'password',
        'sex',
        'birthyear',
        'city'
    ];

    /**
     * The attributes excluded from the model's JSON form.
     *
     * @var array
     */
    protected $hidden = [
        'password', 'remember_token'
    ];

    // Custom attributes also added to the array
    protected $appends = ['full_name', 'num_opinions'];

    public function proposalOpinions()
    {
        return $this->hasMany(ProposalOpinion::class);
    }

    public function notifications()
    {
        return $this->hasMany(Notification::class);
    }

	public function getNumOpinionsAttribute() {
		return $this->proposalOpinions->count();
	}

    public function setPasswordAttribute($value) {
        if($value != "") {
            $this->attributes['password'] = bcrypt($value);
        }
    }

    public function getFullNameAttribute() {
        return $this->firstname . ' ' . $this->lastname;
    }
}
