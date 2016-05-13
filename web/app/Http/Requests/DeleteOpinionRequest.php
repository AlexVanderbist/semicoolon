<?php

namespace App\Http\Requests;

use App\Http\Requests\Request;
use App\Opinion;

class DeleteOpinionRequest extends Request
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        $opinionId = $this->route('opinion');

        $isUserOpinionPoster = Opinion::where('id', $opinionId)
                      ->where('user_id', Auth::id())->exists();

        return $isUserOpinionPoster || Auth::user()->admin;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules()
    {
        return [
            //
        ];
    }
}
