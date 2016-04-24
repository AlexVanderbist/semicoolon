<?php

namespace App\Http\Requests;

use App\Http\Requests\Request;

class DeleteUserRequest extends Request
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        if($this->route('users') == auth()->user()->id) {
            return false;
        }

        return true;
    }

    public function forbiddenResponse() {
        return redirect()->back()->withErrors([
            'error' => 'Je kunt jezelf niet verwijderen.'
        ]);
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
