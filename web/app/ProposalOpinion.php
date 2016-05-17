<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class ProposalOpinion extends Model
{

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'value',
        'type',
        'user_id',
        'proposal_id'
    ];

    public function scopeOfType($query, $type)
    {
        return $query->where('type', $type);
    }

    public function scopeWithValue($query, $value)
    {
        return $query->where('value', $value);
    }

    public function proposal()
    {
        return $this->belongsTo(Proposal::class);
    }

    public function user()
    {
        return $this->belongsTo(User::class);
    }
}
