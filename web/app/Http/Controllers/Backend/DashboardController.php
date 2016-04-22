<?php

namespace App\Http\Controllers\Backend;

use App\Http\Requests;
use Illuminate\Http\Request;

class DashboardController extends Controller
{
    public function index() {

        return view('backend.dashboard');
    }

}
