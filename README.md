# Jeton

Token Base Auth. Service

<div class="row">
    <div class="col-md-12">
        <h2>Getting started</h2>
        <p>
            ASP.NET Web API is a framework that makes it easy to build HTTP services that reach
            a broad range of clients, including browsers and mobile devices. ASP.NET Web API
            is an ideal platform for building RESTful applications on the .NET Framework.
        </p>
    </div>

    <div class="col-md-12">

        <div class="page-header">
            <h1>Introduction<small></small></h1>
            <h2>Generate Token <small>(POST)</small></h2>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">  URI (AppId)</h3>
                </div>
                <div class="panel-body">

                    <pre>http://[domain]/api/token/generate/[AppId]</pre>

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Parameter</th>
                                <th>Value Type</th>
                                <th>Sample Value</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">AppId</th>
                                <td>Guid</td>
                                <td>{F20342AD-8790-E611-8197-94659CB4C67E}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Header (AccessKey)</h3>
                </div>
                <div class="panel-body">

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Parameter</th>
                                <th>Value Type</th>
                                <th>Sample Value</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">AccessKey</th>
                                <td>String</td>
                                <td>BkX9uAhkHoRXTrkPbyZHsx48Odyeh</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Body (UserName,UserNameId)</h3>
                </div>
                <div class="panel-body">

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Parameter</th>
                                <th>Value Type</th>
                                <th>Sample Value</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">UserName</th>
                                <td>String</td>
                                <td>contoso/bgates</td>
                            </tr>
                            <tr>
                                <th scope="row">UserNameId</th>
                                <td>String</td>
                                <td>s-1-5-21-1974876067-1066505772-4238931308-1609</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Response</h3>
                </div>
                <div class="panel-body">

<pre>{
  "TokenKey": "0TzC5S0p4RZPwmFaR3MXTFskWbjMA//fZwBNGgbugPIPywKrpVPQnR6ByGT/yP/W5q6xWFhGdlq+WqOA20SnGWQIGwo/NThZ+KzcMlNt2Ygai/liTEf/KEi49PV4Fnn0mZFzUjnWss3zQw/HadmCfTjnKl6a+5NCBTsctYUZDUDcyse+tRavtT4gAI2KY2mairoRWrpr6DEW8DyxaCafJcBApDQ97mAUKQGtFClPeLJsqT10zaMFVasm5BKRrpt1"
}
</pre>
                </div>
            </div>

        </div>

        <div class="page-header">
            <h2>Check Token Is Active <small>(POST)</small></h2>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">URI </h3>
                </div>
                <div class="panel-body">

                    <pre>http://[domain]/api/token/check/[AppId]</pre>

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Parameter</th>
                                <th>Value Type</th>
                                <th>Sample Value</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">AppId</th>
                                <td>Guid</td>
                                <td>{F20342AD-8790-E611-8197-94659CB4C67E}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Header</h3>
                </div>
                <div class="panel-body">

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Parameter</th>
                                <th>Value Type</th>
                                <th>Sample Value</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">AccessKey</th>
                                <td>String</td>
                                <td>BkX9uAhkHoRXTrkPbyZHsx48Odyeh</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Body</h3>
                </div>
                <div class="panel-body">

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Parameter</th>
                                <th>Value Type</th>
                                <th>Sample Value</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th scope="row">TokenKey</th>
                                <td>String</td>
                                <td style="word-wrap: break-word; max-width: 560px;">OllZ0wkJasau54vpR4RdN65uvcSpG11JGY33uWKK7TMeSvuQA4tHkIktrkMOw+9chki0g7gx+CR2oOomHFn+FXRaY4DgsOjL7+PMFUT4+F6jac4c4IkuUw3lWS4pscZn+psmgg+xsyAMwZZ2R4sd2IWq7dWd+G1pEKkslnxAM/ddjie9fpeF9CDFUy6VoIoT0CYG6LCDrLeaPm2A7vWgHC4HQkI4OLmxrJMNhEge7QmUTmnzMbN0o45yVJXKbU9p</td>
                            </tr>
                            
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Response</h3>
                </div>
                <div class="panel-body">

<pre>{
    "IsActive": true,
    "UserName": "contoso\\bgates",
    "UserNameId": "s-1-5-21-1974876067-1066505772-4238931308-1609"
}
</pre>
                </div>
            </div>


        </div>


    </div>

</div>
